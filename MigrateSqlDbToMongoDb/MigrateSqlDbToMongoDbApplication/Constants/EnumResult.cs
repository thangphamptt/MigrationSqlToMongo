namespace MigrateSqlDbToMongoDbApplication.Constants
{
	public enum EnumResult
	{
		New = 1048576,
		Open = 2097152,
		Rejected = 4194304,
		Passed = 8388608,
		OnHold = 16777216,
		BlackList = 33554432,
		Shortlist = 8192,
		RejectAll = 16384
	};

	public enum EnumApplicationRound
	{
		ScreeeningCv = 1,
		FirstInterview = 2,
		SecondInterview = 4,
		ThirdInterview = 8
	}

	public enum EnumOfferStatus
	{
		HasOffer = 65536,
		AlreadySent = 67108864,
		AcceptOffer = 134217728,
		RejectOffer = 268435456
	}

	public enum EnumScreeningCv
	{
		New = 1048577,
		Open = 2097153,
		Rejected = 4194305,
		Passed = 8388609,
		OnHold = 16777216,
		BlackList = 33554433
	}

	public enum EnumFirstInterview
	{
		New = 1048578,
		Rejected = 4194306,
		Passed = 8388610
	}
	public enum EnumSecondInterview
	{
		New = 1048580,
		Rejected = 4194308,
		Passed = 8388612
	}
	public enum EnumThirdInterview
	{
		New = 1048584,
		Rejected = 4194312,
		Passed = 8388616
	}
	public enum EnumBecomeEmployee
	{
		Completed = 536870912
	}
}
