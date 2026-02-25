using System.Collections.Generic;

namespace ZXing.OneD
{
	internal sealed class EANManufacturerOrgSupport
	{
		private List<int[]> ranges = new List<int[]>();

		private List<string> countryIdentifiers = new List<string>();

		internal string lookupCountryIdentifier(string productCode)
		{
			initIfNeeded();
			int num = int.Parse(productCode.Substring(0, 3));
			int count = ranges.Count;
			for (int i = 0; i < count; i++)
			{
				int[] array = ranges[i];
				int num2 = array[0];
				if (num < num2)
				{
					return null;
				}
				int num3 = ((array.Length != 1) ? array[1] : num2);
				if (num <= num3)
				{
					return countryIdentifiers[i];
				}
			}
			return null;
		}

		private void add(int[] range, string id)
		{
			ranges.Add(range);
			countryIdentifiers.Add(id);
		}

		private void initIfNeeded()
		{
			if (ranges.Count == 0)
			{
				add(new int[2] { 0, 19 }, "US/CA");
				add(new int[2] { 30, 39 }, "US");
				add(new int[2] { 60, 139 }, "US/CA");
				add(new int[2] { 300, 379 }, "FR");
				add(new int[1] { 380 }, "BG");
				add(new int[1] { 383 }, "SI");
				add(new int[1] { 385 }, "HR");
				add(new int[1] { 387 }, "BA");
				add(new int[2] { 400, 440 }, "DE");
				add(new int[2] { 450, 459 }, "JP");
				add(new int[2] { 460, 469 }, "RU");
				add(new int[1] { 471 }, "TW");
				add(new int[1] { 474 }, "EE");
				add(new int[1] { 475 }, "LV");
				add(new int[1] { 476 }, "AZ");
				add(new int[1] { 477 }, "LT");
				add(new int[1] { 478 }, "UZ");
				add(new int[1] { 479 }, "LK");
				add(new int[1] { 480 }, "PH");
				add(new int[1] { 481 }, "BY");
				add(new int[1] { 482 }, "UA");
				add(new int[1] { 484 }, "MD");
				add(new int[1] { 485 }, "AM");
				add(new int[1] { 486 }, "GE");
				add(new int[1] { 487 }, "KZ");
				add(new int[1] { 489 }, "HK");
				add(new int[2] { 490, 499 }, "JP");
				add(new int[2] { 500, 509 }, "GB");
				add(new int[1] { 520 }, "GR");
				add(new int[1] { 528 }, "LB");
				add(new int[1] { 529 }, "CY");
				add(new int[1] { 531 }, "MK");
				add(new int[1] { 535 }, "MT");
				add(new int[1] { 539 }, "IE");
				add(new int[2] { 540, 549 }, "BE/LU");
				add(new int[1] { 560 }, "PT");
				add(new int[1] { 569 }, "IS");
				add(new int[2] { 570, 579 }, "DK");
				add(new int[1] { 590 }, "PL");
				add(new int[1] { 594 }, "RO");
				add(new int[1] { 599 }, "HU");
				add(new int[2] { 600, 601 }, "ZA");
				add(new int[1] { 603 }, "GH");
				add(new int[1] { 608 }, "BH");
				add(new int[1] { 609 }, "MU");
				add(new int[1] { 611 }, "MA");
				add(new int[1] { 613 }, "DZ");
				add(new int[1] { 616 }, "KE");
				add(new int[1] { 618 }, "CI");
				add(new int[1] { 619 }, "TN");
				add(new int[1] { 621 }, "SY");
				add(new int[1] { 622 }, "EG");
				add(new int[1] { 624 }, "LY");
				add(new int[1] { 625 }, "JO");
				add(new int[1] { 626 }, "IR");
				add(new int[1] { 627 }, "KW");
				add(new int[1] { 628 }, "SA");
				add(new int[1] { 629 }, "AE");
				add(new int[2] { 640, 649 }, "FI");
				add(new int[2] { 690, 695 }, "CN");
				add(new int[2] { 700, 709 }, "NO");
				add(new int[1] { 729 }, "IL");
				add(new int[2] { 730, 739 }, "SE");
				add(new int[1] { 740 }, "GT");
				add(new int[1] { 741 }, "SV");
				add(new int[1] { 742 }, "HN");
				add(new int[1] { 743 }, "NI");
				add(new int[1] { 744 }, "CR");
				add(new int[1] { 745 }, "PA");
				add(new int[1] { 746 }, "DO");
				add(new int[1] { 750 }, "MX");
				add(new int[2] { 754, 755 }, "CA");
				add(new int[1] { 759 }, "VE");
				add(new int[2] { 760, 769 }, "CH");
				add(new int[1] { 770 }, "CO");
				add(new int[1] { 773 }, "UY");
				add(new int[1] { 775 }, "PE");
				add(new int[1] { 777 }, "BO");
				add(new int[1] { 779 }, "AR");
				add(new int[1] { 780 }, "CL");
				add(new int[1] { 784 }, "PY");
				add(new int[1] { 785 }, "PE");
				add(new int[1] { 786 }, "EC");
				add(new int[2] { 789, 790 }, "BR");
				add(new int[2] { 800, 839 }, "IT");
				add(new int[2] { 840, 849 }, "ES");
				add(new int[1] { 850 }, "CU");
				add(new int[1] { 858 }, "SK");
				add(new int[1] { 859 }, "CZ");
				add(new int[1] { 860 }, "YU");
				add(new int[1] { 865 }, "MN");
				add(new int[1] { 867 }, "KP");
				add(new int[2] { 868, 869 }, "TR");
				add(new int[2] { 870, 879 }, "NL");
				add(new int[1] { 880 }, "KR");
				add(new int[1] { 885 }, "TH");
				add(new int[1] { 888 }, "SG");
				add(new int[1] { 890 }, "IN");
				add(new int[1] { 893 }, "VN");
				add(new int[1] { 896 }, "PK");
				add(new int[1] { 899 }, "ID");
				add(new int[2] { 900, 919 }, "AT");
				add(new int[2] { 930, 939 }, "AU");
				add(new int[2] { 940, 949 }, "AZ");
				add(new int[1] { 955 }, "MY");
				add(new int[1] { 958 }, "MO");
			}
		}
	}
}
