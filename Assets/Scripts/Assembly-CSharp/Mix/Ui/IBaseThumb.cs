using Mix.Data;

namespace Mix.Ui
{
	public interface IBaseThumb
	{
		void OnBaseThumbClicked(BaseContentData aEntitlement, object aUserData);

		void OnBaseThumbLoaded(bool error);
	}
}
