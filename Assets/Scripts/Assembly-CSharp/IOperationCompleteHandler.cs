using Mix.SequenceOperations;

public interface IOperationCompleteHandler
{
	void OnOperationComplete(SequenceOperation aOperation);
}
