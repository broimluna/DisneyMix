using System;
using System.Collections.Generic;

public class FireEventParameters
{
	internal Dictionary<string, object> valuePayload;

	internal string eventName;

	internal Dictionary<FireEventType, string> eventNameList;

	public string checkoutAsGuest
	{
		set
		{
			valuePayload.Add("checkout_as_guest", value ?? string.Empty);
		}
	}

	public string contentId
	{
		set
		{
			valuePayload.Add("content_id", value ?? string.Empty);
		}
	}

	public string contentType
	{
		set
		{
			valuePayload.Add("content_type", value ?? string.Empty);
		}
	}

	public string currency
	{
		set
		{
			valuePayload.Add("currency", value ?? string.Empty);
		}
	}

	public string dateString
	{
		set
		{
			if (!valuePayload.ContainsKey("now_date"))
			{
				valuePayload.Add("now_date", value ?? string.Empty);
			}
		}
	}

	public DateTime date
	{
		set
		{
			if (!valuePayload.ContainsKey("now_date"))
			{
				valuePayload.Add("now_date", value.ToUniversalTime().ToString() ?? string.Empty);
			}
		}
	}

	public string description
	{
		set
		{
			valuePayload.Add("description", value ?? string.Empty);
		}
	}

	public string destination
	{
		set
		{
			valuePayload.Add("destination", value ?? string.Empty);
		}
	}

	public float duration
	{
		set
		{
			valuePayload.Add("duration", value);
		}
	}

	public string endDateString
	{
		set
		{
			if (!valuePayload.ContainsKey("end_date"))
			{
				valuePayload.Add("end_date", value ?? string.Empty);
			}
		}
	}

	public DateTime endDate
	{
		set
		{
			if (!valuePayload.ContainsKey("end_date"))
			{
				valuePayload.Add("end_date", value.ToUniversalTime().ToString() ?? string.Empty);
			}
		}
	}

	public string itemAddedFrom
	{
		set
		{
			valuePayload.Add("item_added_from", value ?? string.Empty);
		}
	}

	public string level
	{
		set
		{
			valuePayload.Add("level", value ?? string.Empty);
		}
	}

	public float maxRatingValue
	{
		set
		{
			valuePayload.Add("max_rating_value", value);
		}
	}

	public string name
	{
		set
		{
			valuePayload.Add("name", value ?? string.Empty);
		}
	}

	public string orderId
	{
		set
		{
			valuePayload.Add("order_id", value ?? string.Empty);
		}
	}

	public string origin
	{
		set
		{
			valuePayload.Add("origin", value ?? string.Empty);
		}
	}

	public float price
	{
		set
		{
			valuePayload.Add("price", value);
		}
	}

	public string quantity
	{
		set
		{
			valuePayload.Add("quantity", value ?? string.Empty);
		}
	}

	public float ratingValue
	{
		set
		{
			valuePayload.Add("rating_value", value);
		}
	}

	public string receiptId
	{
		set
		{
			valuePayload.Add("receipt_id", value ?? string.Empty);
		}
	}

	public string referralFrom
	{
		set
		{
			valuePayload.Add("referral_from", value ?? string.Empty);
		}
	}

	public string registrationMethod
	{
		set
		{
			valuePayload.Add("registration_method", value ?? string.Empty);
		}
	}

	public string results
	{
		set
		{
			valuePayload.Add("results", value ?? string.Empty);
		}
	}

	public string score
	{
		set
		{
			valuePayload.Add("score", value ?? string.Empty);
		}
	}

	public string searchTerm
	{
		set
		{
			valuePayload.Add("search_term", value ?? string.Empty);
		}
	}

	public string startDateString
	{
		set
		{
			if (!valuePayload.ContainsKey("start_date"))
			{
				valuePayload.Add("start_date", value ?? string.Empty);
			}
		}
	}

	public DateTime startDate
	{
		set
		{
			if (!valuePayload.ContainsKey("start_date"))
			{
				valuePayload.Add("start_date", value.ToUniversalTime().ToString() ?? string.Empty);
			}
		}
	}

	public string success
	{
		set
		{
			valuePayload.Add("success", value ?? string.Empty);
		}
	}

	public string userId
	{
		set
		{
			valuePayload.Add("user_id", value ?? string.Empty);
		}
	}

	public string userName
	{
		set
		{
			valuePayload.Add("user_name", value ?? string.Empty);
		}
	}

	public string validated
	{
		set
		{
			valuePayload.Add("validated", value ?? string.Empty);
		}
	}

	public FireEventParameters(FireEventType fireEventType)
	{
		valuePayload = new Dictionary<string, object>();
		eventNameList = new Dictionary<FireEventType, string>
		{
			{
				FireEventType.Achievement,
				"Achievement"
			},
			{
				FireEventType.AddToCart,
				"Add to Cart"
			},
			{
				FireEventType.AddToWishList,
				"Add to Wish List"
			},
			{
				FireEventType.CheckoutStart,
				"Checkout Start"
			},
			{
				FireEventType.LevelComplete,
				"Level Complete"
			},
			{
				FireEventType.Purchase,
				"Purchase"
			},
			{
				FireEventType.Rating,
				"Rating"
			},
			{
				FireEventType.RegistrationComplete,
				"Registration Complete"
			},
			{
				FireEventType.Search,
				"Search"
			},
			{
				FireEventType.TutorialComplete,
				"Tutorial Complete"
			},
			{
				FireEventType.View,
				"View"
			}
		};
		if (!eventNameList.TryGetValue(fireEventType, out eventName))
		{
			eventName = string.Empty;
		}
	}
}
