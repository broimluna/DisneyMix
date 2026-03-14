using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class LinkedChildrenGetter
	{
		public static void GetChildren(AbstractLogger logger, IGuestControllerClient guestControllerClient, IMixWebCallFactory mixWebCallFactory, Action<IGetLinkedUsersResult> callback)
		{
			try
			{
				guestControllerClient.GetLinkedChildren(delegate(GuestControllerResult<ChildrenResponse> r)
				{
					HandleGetLinkedChildrenResult(logger, mixWebCallFactory, callback, r);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(MakeGenericFailure());
			}
		}

		private static void HandleGetLinkedChildrenResult(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, Action<IGetLinkedUsersResult> callback, GuestControllerResult<ChildrenResponse> result)
		{
			try
			{
				if (!result.Success || result.Response.error != null)
				{
					callback(MakeGenericFailure());
					return;
				}
				List<Profile> list = ((result.Response.data != null) ? result.Response.data.children : null);
				if (list == null)
				{
					callback(MakeGenericFailure());
					return;
				}
				LinkedUsersGetter.Get(logger, mixWebCallFactory, list, delegate(LinkedUser[] users)
				{
					Action<IGetLinkedUsersResult> action = callback;
					object obj;
					if (users == null)
					{
						IGetLinkedUsersResult getLinkedUsersResult = MakeGenericFailure();
						obj = getLinkedUsersResult;
					}
					else
					{
						obj = new GetLinkedUsersResult(true, users);
					}
					action((IGetLinkedUsersResult)obj);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(MakeGenericFailure());
			}
		}

		private static IGetLinkedUsersResult MakeGenericFailure()
		{
			return new GetLinkedUsersResult(false, Enumerable.Empty<ILinkedUser>());
		}
	}
}
