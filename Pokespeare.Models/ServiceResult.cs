namespace Pokespeare.Models
{
	public class ServiceResult
	{
		#region Constructors

		public ServiceResult()
		{
		}

		public ServiceResult(Result result, string msg) : this()
		{
			Result = result;
			Message = msg;
		}

		#endregion

		#region Properties

		public Result Result { get; set; }

		public string Message { get; set; }

		#endregion
	}

	public class ServiceResult<T> : ServiceResult
	{
		#region Constructors

		public ServiceResult() : base()
		{
		}

		public ServiceResult(Result result, string msg, T content) : base(result, msg)
		{
			Content = content;
		}

		#endregion

		#region Properties

		public T Content { get; set; }

		#endregion
	}
}
