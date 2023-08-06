using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ConstructionDesign
{
	public static class revittools
	{
        public static double ft2mm(this double ft)
        {
            return UnitUtils.ConvertFromInternalUnits(ft, DisplayUnitType.DUT_MILLIMETERS);
        }
        public static double mm2ft(this double ft)
        {
            return UnitUtils.ConvertToInternalUnits(ft, DisplayUnitType.DUT_MILLIMETERS);
        }
    }
}
