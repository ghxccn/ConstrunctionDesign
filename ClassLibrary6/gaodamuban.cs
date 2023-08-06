using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

namespace ConstructionDesign
{
    [Transaction(TransactionMode.Manual)]
    public class gaodamuban : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc1 = commandData.Application.ActiveUIDocument;
            Document doc1 = uidoc1.Document;
            //获得文档；

            FilteredElementCollector fec1 = new FilteredElementCollector(doc1);
            IList<Element> beamlist1 = fec1.OfCategory(BuiltInCategory.OST_StructuralFraming).OfClass(typeof(FamilyInstance)).ToElements();

			FilteredElementCollector fillPatterFilter = new FilteredElementCollector(doc1);
			fillPatterFilter.OfClass(typeof(FillPatternElement));
			FillPatternElement fp = fillPatterFilter.First(m => m.Name == "<实体填充>") as FillPatternElement;

			using (Transaction tran1 = new Transaction(doc1, "高大模板标记"))
			{
				tran1.Start();
				for (int i = 0; i < beamlist1.Count; i++)
				{

					if (gdmbjudgement(beamlist1[i],doc1))	
					{
						OverrideGraphicSettings ogs1 = new OverrideGraphicSettings();
						ogs1 = doc1.ActiveView.GetElementOverrides(beamlist1[i].Id);
						ogs1.SetSurfaceForegroundPatternId(fp.Id);
						ogs1.SetSurfaceForegroundPatternColor(new Color(255, 0, 0));
						doc1.ActiveView.SetElementOverrides(beamlist1[i].Id, ogs1);
					}
				}
				doc1.Regenerate();
				tran1.Commit();
			}
			return Result.Succeeded;
		}

		private bool gdmbjudgement(Element ele, Document doc)
		{
			if ((ele.Category.Name == Category.GetCategory(doc, BuiltInCategory.OST_StructuralFraming).Name))
			{
				FamilyInstance fi = ele as FamilyInstance;
				FamilySymbol fs = fi.Symbol;
				double b = fs.LookupParameter("b").AsDouble().ft2mm();
				double h = fs.LookupParameter("h").AsDouble().ft2mm();
				if (b * h > 769231)
				{
					return true;
				}
			}
			return false;
		}
	}
}
