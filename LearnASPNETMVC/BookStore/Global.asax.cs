﻿namespace BookStore
{
	using BookStore.Db;
	using BookStore.Db.Contexts;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{
		protected async void Application_Start()
		{
			await DatabaseEntityFramework.InvokeLearnQueryes();
			DatabaseViaCommand.InvokeLearnQueryes();
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
