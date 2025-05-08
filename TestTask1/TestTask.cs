using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

public class TestTask : IExternalApplication
{
    public Result OnStartup(UIControlledApplication application)
    {
        application.ControlledApplication.DocumentOpened += OnDocumentOpened;
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        application.ControlledApplication.DocumentOpened -= OnDocumentOpened;
        return Result.Succeeded;
    }

    private void OnDocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
    {
        Document doc = e.Document;

        using (Transaction tx = new Transaction(doc, "Add Comments"))
        {
            tx.Start();

            var rebars = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rebar);

            foreach (var rebar in rebars)
            {
                Parameter commentParam = rebar.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

                string comment = "";

                if (rebar is Rebar rebarElement)
                {
                    Parameter lengthParam = rebarElement.get_Parameter(BuiltInParameter.REBAR_ELEM_TOTAL_LENGTH);
                    if (lengthParam != null)
                    {
                        ElementId typeId = rebarElement.GetTypeId();
                        RebarBarType barType = doc.GetElement(typeId) as RebarBarType;
                        string dim = barType.get_Parameter(BuiltInParameter.REBAR_BAR_DIAMETER).AsValueString();
                        comment = "Ø" + dim + ", Grade 60, L=" + lengthParam.AsValueString();
                    }
                }
                else if (rebar is FamilyInstance familyInstance)
                {
                    string length = familyInstance.LookupParameter("L")?.AsValueString();
                    string dim = familyInstance.LookupParameter("Rebar Diameter")?.AsValueString();
                    if (length != null && dim != null)
                    {
                        comment = "Ø" + dim + ", Grade 60, L=" + length;
                    }
                }

                if (commentParam != null)
                {
                    commentParam.Set(comment);
                }
                    
            }


            tx.Commit();
        }
    }
}
