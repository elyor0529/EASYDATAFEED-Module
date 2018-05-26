using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Scraper.Shared;
using System.Web;
using HtmlAgilityPack;
using TestModule1;
using Databox.Libs.TestModule1;

namespace WheelsScraper
{
	public class TestModule1 : BaseScraper
	{
		public TestModule1()
		{
			Name = "TestModule1";
			Url = "https://www.TestModule1.com/";
			PageRetriever.Referer = Url;
			WareInfoList = new List<ExtWareInfo>();
			Wares.Clear();
			BrandItemType = 2;

			SpecialSettings = new ExtSettings();
		}

		private ExtSettings extSett
		{
			get
			{
				return (ExtSettings)Settings.SpecialSettings;
			}
		}

		public override Type[] GetTypesForXmlSerialization()
		{
			return new Type[] { typeof(ExtSettings) };
		}

		//public override System.Windows.Forms.Control SettingsTab
		//{
		//	get
		//	{
		//		var frm = new ucExtSettings();
		//		frm.Sett = Settings;
		//		return frm;
		//	}
		//}

		public override WareInfo WareInfoType
		{
			get
			{
				return new ExtWareInfo();
			}
		}

		protected override bool Login()
		{
			return true;
		}

		protected override void RealStartProcess()
		{
			lstProcessQueue.Add(new ProcessQueueItem { URL = Url, ItemType = 1 });
			StartOrPushPropertiesThread();
		}

		protected void ProcessBrandsListPage(ProcessQueueItem pqi)
		{
			if (cancel)
				return;
			AddWareInfo(new ExtWareInfo { Brand = "Test Brand", Name = "Test product Name" });

			pqi.Processed = true;
			MessagePrinter.PrintMessage("Brands list processed");
			StartOrPushPropertiesThread();
		}

		protected override Action<ProcessQueueItem> GetItemProcessor(ProcessQueueItem item)
		{
			Action<ProcessQueueItem> act;
			if (item.ItemType == 1)
				act = ProcessBrandsListPage;
			else act = null;

			return act;
		}
	}
}
