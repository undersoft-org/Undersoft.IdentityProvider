﻿using System;
using System.Collections.Generic;
using System.Linq;
using Undersoft.IDP.Admin.BusinessLogic.Shared.Dtos.Common;

namespace Undersoft.IDP.Admin.BusinessLogic.Helpers
{
	public class EnumHelpers
	{
		public static List<SelectItemDto> ToSelectList<T>() where T : struct, IComparable
		{
			var selectItems = Enum.GetValues(typeof(T))
				.Cast<T>()
				.Select(x => new SelectItemDto(Convert.ToInt16(x).ToString(), x.ToString())).ToList();

			return selectItems;
		}
	}
}