namespace backnc.Common.Response
{
	public class BaseResponse
	{
		public string status { get; set; } = "success";
		public string message { get; set; }
		public object data { get; set; }
		public bool IsSuccess { get; set; } = true;

		public BaseResponse() { }

		public BaseResponse(object data)
		{
			this.data = data;
		}

		public BaseResponse(string message)
		{
			this.message = message;
		}
		public BaseResponse(string message, object data)
		{
			this.message = message;
			this.data = data;
		}

		public BaseResponse(string message, object data, bool errorbandera)
		{
			if (errorbandera)
			{
				this.status = "error";
				this.message = message;
				this.data = data;
				this.IsSuccess = false;
			}
			else
			{
				this.message = message;
				this.data = data;
				this.IsSuccess = true;
			}
		}

		// Con validationError
		public BaseResponse(object errors, string message = "Error de validación")
		{
			this.status = "error";
			this.message = message;
			this.data = errors;
			this.IsSuccess = false;
		}
	}

	public static class Response
	{
		public static BaseResponse Success()
		{
			return new BaseResponse();
		}

		public static BaseResponse Success(object data)
		{
			return new BaseResponse(data);
		}

		public static BaseResponse Success(string mensaje)
		{
			return new BaseResponse(mensaje);
		}

		public static BaseResponse Success(string mensaje, object data, bool errorbandera = false)
		{
			return new BaseResponse(mensaje, data, errorbandera);
		}

		public static BaseResponse ValidationError(string mensaje, object errors, bool errorbandera = true)
		{
			return new BaseResponse(mensaje, errors, errorbandera);
		}
	}
}