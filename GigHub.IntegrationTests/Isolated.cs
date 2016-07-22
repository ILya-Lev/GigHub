﻿using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace GigHub.IntegrationTests
{
	[AttributeUsage(AttributeTargets.Method)]
	public class IsolatedAttribute : Attribute, ITestAction
	{
		private TransactionScope _transactionScope;
		public void BeforeTest(ITest test)
		{
			_transactionScope = new TransactionScope();
		}

		public void AfterTest(ITest test)
		{
			_transactionScope.Dispose();
		}

		public ActionTargets Targets => ActionTargets.Test;
	}
}
