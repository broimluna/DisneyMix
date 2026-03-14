using System.Text;

namespace ZXing.Client.Result
{
	public class VINParsedResult : ParsedResult
	{
		public string VIN { get; private set; }

		public string WorldManufacturerID { get; private set; }

		public string VehicleDescriptorSection { get; private set; }

		public string VehicleIdentifierSection { get; private set; }

		public string CountryCode { get; private set; }

		public string VehicleAttributes { get; private set; }

		public int ModelYear { get; private set; }

		public char PlantCode { get; private set; }

		public string SequentialNumber { get; private set; }

		public override string DisplayResult
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(50);
				stringBuilder.Append(WorldManufacturerID).Append(' ');
				stringBuilder.Append(VehicleDescriptorSection).Append(' ');
				stringBuilder.Append(VehicleIdentifierSection).Append('\n');
				if (CountryCode != null)
				{
					stringBuilder.Append(CountryCode).Append(' ');
				}
				stringBuilder.Append(ModelYear).Append(' ');
				stringBuilder.Append(PlantCode).Append(' ');
				stringBuilder.Append(SequentialNumber).Append('\n');
				return stringBuilder.ToString();
			}
		}

		public VINParsedResult(string vin, string worldManufacturerID, string vehicleDescriptorSection, string vehicleIdentifierSection, string countryCode, string vehicleAttributes, int modelYear, char plantCode, string sequentialNumber)
			: base(ParsedResultType.VIN)
		{
			VIN = vin;
			WorldManufacturerID = worldManufacturerID;
			VehicleDescriptorSection = vehicleDescriptorSection;
			VehicleIdentifierSection = vehicleIdentifierSection;
			CountryCode = countryCode;
			VehicleAttributes = vehicleAttributes;
			ModelYear = modelYear;
			PlantCode = plantCode;
			SequentialNumber = sequentialNumber;
		}
	}
}
