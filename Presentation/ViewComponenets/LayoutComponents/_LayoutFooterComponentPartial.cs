﻿using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.LayoutComponents
{
	public class _LayoutFooterComponentPartial:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
