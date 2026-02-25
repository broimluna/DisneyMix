using System;
using System.Linq;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ClaimableChildrenGetter
	{
		public static void GetChildren(AbstractLogger logger, IGuestControllerClient guestControllerClient, IMixWebCallFactory mixWebCallFactory, Action<IGetLinkedUsersResult> callback)
		{
			try
			{
				guestControllerClient.GetClaimableChildren(delegate(GuestControllerResult<ChildrenResponse> r)
				{
					HandleGetClaimableChildrenResult(logger, mixWebCallFactory, callback, r);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(MakeGenericFailure());
			}
		}

		private static void HandleGetClaimableChildrenResult(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, Action<IGetLinkedUsersResult> callback, GuestControllerResult<ChildrenResponse> result)
		{
			try
			{
				if (!result.Success || result.Response.error != null || result.Response.data == null)
				{
					callback(MakeGenericFailure());
					return;
				}
				LinkedUsersGetter.Get(logger, mixWebCallFactory, result.Response.data.children, delegate(LinkedUser[] users)
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
