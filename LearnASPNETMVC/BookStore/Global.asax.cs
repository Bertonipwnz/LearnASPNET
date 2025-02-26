﻿namespace BookStore
{
	using BookStore.Db;
	using BookStore.Db.Contexts;
	using System.Data.Entity;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			//Database.SetInitializer(new BookDbInitializer());
			DatabaseViaCommand.CreateBookTable();
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
