using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ConstructionDesign
{
	[Transaction(TransactionMode.Manual)]
	public class RBB : IExternalApplication
	{
		public Result OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		public Result OnStartup(UIControlledApplication application)
		{
			application.CreateRibbonTab("施工设计工具箱");
			RibbonPanel rp1 = application.CreateRibbonPanel("施工设计工具箱", "专项方案");
			PushButtonData pbd1 = new PushButtonData("高大模板", "高大模板标记", @"E:\OneDrive\model\C#\3. Product\施工设计工具\ClassLibrary6\bin\Debug\ClassLibrary6.dll", "ConstructionDesign.gaodamuban");
			PushButton pb1 = rp1.AddItem(pbd1) as PushButton;
			pb1.LargeImage = new BitmapImage(new Uri(@"C:\Users\user\Desktop\字母Gb.jpg"));
			pb1.ToolTipImage = new BitmapImage(new Uri(@"C:\Users\user\Desktop\字母Gs.jpg"));
			pb1.ToolTip = "自动标记工程中高大模板构件";

			return Result.Succeeded;
		}
	}
}
