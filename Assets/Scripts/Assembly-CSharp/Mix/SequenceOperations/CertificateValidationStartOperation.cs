using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Mix.SequenceOperations
{
	public class CertificateValidationStartOperation : SequenceOperation
	{
		public CertificateValidationStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain c2, SslPolicyErrors s2) => true;
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
