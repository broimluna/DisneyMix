using Mix.Data;

namespace Mix.Ui
{
	public interface IMediaTray
	{
		void OnEntitlementClicked(BaseContentData aEntitlement);

		void OnContentHolderChanged();

		void OnShowPreviewPanel(BaseContentData aEntitlement);

		void OnHidePreviewPanel();
	}
}
