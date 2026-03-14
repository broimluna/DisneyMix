using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Avatar;
using Disney.Mix.SDK;
using Disney.Native;
using Mix.Avatar;
using Mix.DeviceDb;
using Mix.Localization;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

namespace Mix.Ui
{
	public class QRCodePanel : BasePanel, InviteItem.IInviteListener
	{
		public GameObject searchCameraPanel;

		public GameObject cameraPlane;

		public Image usersQRCodeImage;

		public GameObject trustedRequestItem;

		public AvatarObjectSpawner AvatarSpawner;

		public Text displayName;

		[Space(5f)]
		public bool useCustomQRColors;

		public Color customQRBlackColor = Color.black;

		public Color customQRWhiteColor = Color.white;

		[Space(5f)]
		public AnimationCurve ScannerEffect;

		private BarcodeReader barcodeReader;

		private Result QRResult;

		private WebCamTexture webCamTexture;

		private Color32[] pixels;

		private int width;

		private int height;

		private bool meshAspectRatioAdjusted;

		private Material cameraMaterial;

		private SdkEvents eventGenerator = new SdkEvents();

		private InviteItem.IInviteListener inviteListener;

		private IIncomingFriendInvitation currentInvite;

		private InviteItem inviteItem;

		private GameObject inviteItemInst;

		private GameObject notificationBar;

		private List<IIncomingFriendInvitation> pendingInvitations;

		public event EventHandler<SearchForUserEventArgs> OnSearchForUser = delegate
		{
		};

		public void Init(InviteItem.IInviteListener aInviteListener, bool aDefaultToCode = false)
		{
			inviteListener = aInviteListener;
			searchCameraPanel.SetActive(true);
			cameraMaterial = cameraPlane.GetComponent<MeshRenderer>().material;
			cameraMaterial.shader = Shader.Find("Custom/MIXQRScannerShader");
			if (MixSession.IsValidSession)
			{
				IKeyValDatabaseApi keyValDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
				string text = keyValDocumentCollectionApi.LoadUserValue("default_primary_color");
				cameraMaterial.SetColor("_ScanColor", Util.HexToColor(string.IsNullOrEmpty(text) ? "1C97D4" : text));
			}
			RequestPermission();
			Analytics.LogQRScannerPageView();
			displayName.text = MixSession.User.DisplayName.Text;
			GameObject avatarCube = AvatarSpawner.Init();
			MonoSingleton<AvatarManager>.Instance.SkinAvatar(avatarCube, MixSession.User.Avatar, (AvatarFlags)0, delegate
			{
				if (!this.IsNullOrDisposed() && avatarCube != null)
				{
					avatarCube.SetActive(true);
				}
			});
			EncodeDisplayName();
			if (aDefaultToCode)
			{
				GetComponent<Animator>().Play("QRScreen_QRDefault");
			}
			pendingInvitations = new List<IIncomingFriendInvitation>();
		}

		public void OnApplicationPause(bool aPaused)
		{
			if (aPaused)
			{
				CleanUp();
			}
			else
			{
				RequestPermission(true);
			}
		}

		public void Update()
		{
			if (webCamTexture != null && webCamTexture.isPlaying)
			{
				Quaternion q = Quaternion.Euler(0f, 0f, webCamTexture.videoRotationAngle);
				cameraMaterial.SetInt("_Invert", 0);
				Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, q, new Vector3(1f, 1f, 1f));
				cameraMaterial.SetMatrix("_Rotation", matrix);
			}
			if (QRResult != null && !QRResult.Text.Equals(string.Empty))
			{
				if (MixChat.FindFriendByDisplayName(QRResult.Text) != null)
				{
					GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
					genericPanel.ShowSimpleError(Singleton<Localizer>.Instance.getString("customtokens.friends.user_already_friend"));
				}
				else
				{
					this.OnSearchForUser(this, new SearchForUserEventArgs(QRResult.Text));
				}
				searchCameraPanel.SetActive(false);
				CleanUp();
				QRResult = null;
				ClosePanel();
			}
			cameraMaterial.SetFloat("_ScanOffset", ScannerEffect.Evaluate(Time.time));
		}

		public void Start()
		{
			MixSession.User.OnReceivedIncomingFriendInvitation += eventGenerator.AddEventHandler<AbstractReceivedIncomingFriendInvitationEventArgs>(this, HandleReceivedIncomingFriendInvitation);
			notificationBar = base.transform.Find("QRView/NotificationBar").gameObject;
		}

		public void OnDestroy()
		{
			MixSession.User.OnReceivedIncomingFriendInvitation -= eventGenerator.GetEventHandler<AbstractReceivedIncomingFriendInvitationEventArgs>(this, HandleReceivedIncomingFriendInvitation);
			CleanUp();
		}

		public void HandleReceivedIncomingFriendInvitation(object sender, AbstractReceivedIncomingFriendInvitationEventArgs e)
		{
			if (!this.IsNullOrDisposed() && e != null && e.Invitation != null)
			{
				OnFriendInvite(e.Invitation);
			}
		}

		public void FriendNotificationClicked()
		{
			ClosePanel();
		}

		public void OnCloseButtonClicked()
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_6_AvatarUISelect");
			ClosePanel();
		}

		public void OnFriendInvite(IIncomingFriendInvitation invite)
		{
			if (invite.Invitee != null && base.transform != null && MixChat.FindFriendByDisplayName(invite.Inviter.DisplayName.Text) == null)
			{
				if (currentInvite != null)
				{
					pendingInvitations.Add(currentInvite);
					inviteItem.ResetItem();
					notificationBar.SetActive(false);
					UnityEngine.Object.Destroy(inviteItemInst);
				}
				SetCurrentInvite(invite);
			}
		}

		private void SetCurrentInvite(IIncomingFriendInvitation invite)
		{
			currentInvite = invite;
			inviteItemInst = UnityEngine.Object.Instantiate(trustedRequestItem);
			inviteItemInst.transform.SetParent(notificationBar.transform.Find("ItemTarget"), false);
			inviteItem = new InviteItem(null, invite, invite.RequestTrust, this);
			inviteItem.Init(inviteItemInst);
			inviteItemInst.SetActive(true);
			notificationBar.SetActive(true);
			UpdatingPendingInvites();
		}

		private void UpdatingPendingInvites()
		{
			int num = pendingInvitations.Count() + 1;
			Text component = notificationBar.transform.Find("NotificationTextPost1.0").GetComponent<Text>();
			component.text = Singleton<Localizer>.Instance.getString("qrcodepanel.notificationbar.notificationtextpost1.0").Replace("#firstNumber#", "1").Replace("#secondNumber#", num.ToString());
			component.GetComponent<LocalizedText>().doNotLocalize = true;
		}

		private void CurrentInviteHandled()
		{
			inviteItem.ResetItem();
			inviteItemInst.SetActive(false);
			notificationBar.SetActive(false);
			if (pendingInvitations.Count() > 0)
			{
				IIncomingFriendInvitation incomingFriendInvitation = pendingInvitations[pendingInvitations.Count() - 1];
				pendingInvitations.RemoveAt(pendingInvitations.Count() - 1);
				SetCurrentInvite(incomingFriendInvitation);
			}
			else
			{
				inviteItem = null;
				currentInvite = null;
			}
		}

		public void OnAcceptClicked(IIncomingFriendInvitation aInvite, bool trust)
		{
			inviteListener.OnAcceptClicked(currentInvite, trust);
			CurrentInviteHandled();
		}

		public void OnDeclineClicked(IIncomingFriendInvitation aInvite)
		{
			inviteListener.OnDeclineClicked(currentInvite);
			CurrentInviteHandled();
		}

		private void CleanUp()
		{
			if (webCamTexture != null && webCamTexture.isPlaying)
			{
				webCamTexture.Stop();
				webCamTexture = null;
			}
			StopCoroutine("BeginDecoding");
		}

		private void EncodeDisplayName()
		{
			BarcodeWriter barcodeWriter = new BarcodeWriter();
			barcodeWriter.Format = BarcodeFormat.QR_CODE;
			barcodeWriter.Options = new QrCodeEncodingOptions
			{
				Height = 256,
				Width = 256
			};
			BarcodeWriter barcodeWriter2 = barcodeWriter;
			Texture2D texture2D = new Texture2D(barcodeWriter2.Options.Width, barcodeWriter2.Options.Height);
			texture2D.filterMode = FilterMode.Point;
			string text = MixSession.User.DisplayName.Text;
			Color32[] array = barcodeWriter2.Write(text);
			if (useCustomQRColors)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == Color.black)
					{
						array[i] = customQRBlackColor;
					}
					else
					{
						array[i] = customQRWhiteColor;
					}
				}
			}
			texture2D.SetPixels32(array);
			texture2D.Apply();
			Rect rect = new Rect(0f, 0f, barcodeWriter2.Options.Width, barcodeWriter2.Options.Height);
			usersQRCodeImage.sprite = Sprite.Create(texture2D, rect, Vector2.zero);
		}

		private void RequestPermission(bool aDontPrompt = false)
		{
			searchCameraPanel.SetActive(false);
			searchCameraPanel.transform.GetChild(0).gameObject.SetActive(false);
			if (MonoSingleton<NativeUtilitiesManager>.Instance.Native.HasPermissions(new List<string> { "camera" }))
			{
				UserHasPermission();
			}
			else if (!aDontPrompt && !MonoSingleton<NativeUtilitiesManager>.Instance.Native.AskForPermissions(new List<string> { "camera" }, OnPermissionResult))
			{
				UserDoesntHavePermission();
			}
		}

		private void OnPermissionResult(NativeUtilitiesPermissionResult aResult)
		{
			if (aResult.HasPermission)
			{
				UserHasPermission();
			}
			else
			{
				UserDoesntHavePermission();
			}
		}

		private void UserHasPermission()
		{
			CleanUp();
			searchCameraPanel.SetActive(true);
			searchCameraPanel.transform.GetChild(0).gameObject.SetActive(true);
			StartCoroutine("BeginDecoding");
		}

		private void UserDoesntHavePermission()
		{
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, null },
				{ genericPanel.MessageText, "customtokens.panels.enable_camera" },
				{ genericPanel.ButtonOneText, "customtokens.panels.button_ok" }
			});
		}

		private IEnumerator BeginDecoding()
		{
			webCamTexture = null;
			webCamTexture = new WebCamTexture(0, 0, 30);
			cameraMaterial.mainTexture = webCamTexture;
			webCamTexture.Play();
			barcodeReader = new BarcodeReader
			{
				AutoRotate = true
			};
			barcodeReader.Options.PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE };
			barcodeReader.TryInverted = true;
			yield return new WaitForSeconds(0.5f);
			while (webCamTexture.width <= 16)
			{
				yield return null;
			}
			if (!meshAspectRatioAdjusted)
			{
				AdjustMeshAspectRatio();
				meshAspectRatioAdjusted = true;
			}
			while (true)
			{
				if (webCamTexture.isPlaying && QRResult == null)
				{
					width = webCamTexture.width;
					height = webCamTexture.height;
					pixels = webCamTexture.GetPixels32();
					ThreadPool.QueueUserWorkItem(DecodingWorker, null);
				}
				yield return new WaitForSeconds(0.2f);
			}
		}

		private void AdjustMeshAspectRatio()
		{
			float num = 1f;
			float num2 = 1f;
			if (webCamTexture.width > webCamTexture.height)
			{
				num = (float)webCamTexture.height / (float)webCamTexture.width;
			}
			else if (webCamTexture.width < webCamTexture.height)
			{
				num2 = (float)webCamTexture.width / (float)webCamTexture.height;
			}
			Mesh mesh = cameraPlane.GetComponent<MeshFilter>().mesh;
			Vector2[] uv = mesh.uv;
			Vector2[] array = new Vector2[uv.Length];
			for (int i = 0; i < uv.Length; i++)
			{
				array[i] = uv[i];
				uv[i].x = uv[i].x * num + (1f - num) / 2f;
				uv[i].y = uv[i].y * num2 + (1f - num2) / 2f;
			}
			mesh.uv = uv;
			mesh.uv2 = array;
			cameraPlane.GetComponent<MeshFilter>().sharedMesh = mesh;
		}

		private void DecodingWorker(object state)
		{
			try
			{
				if (pixels != null && pixels.Length > 0 && width > 0 && height > 0)
				{
					QRResult = barcodeReader.Decode(pixels, width, height);
				}
			}
			catch (UnityException)
			{
			}
		}
	}
}
