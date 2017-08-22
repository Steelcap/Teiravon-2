using System;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	public enum BulkMaterialType
	{
		None,
		DullCopper,
		ShadowIron,
		Copper,
		Bronze,
		Gold,
		Agapite,
		Verite,
		Valorite,
		Pine,
		Redwood,
		WhitePine,
		Ashwood,
		SilverBirch,
		Yew,
		BlackOak,
		Spined,
		Horned,
		Barbed
	}

	public enum BulkGenericType
	{
		Iron,
		Cloth,
		Leather,
		Wood
	}

	public class BGTClassifier
	{
		public static BulkGenericType Classify( BODType deedType, Type itemType )
		{
			if ( deedType == BODType.Tailor )
			{
				if ( itemType == null || itemType.IsSubclassOf( typeof( BaseArmor ) ) || itemType.IsSubclassOf( typeof( BaseShoes ) ) )
					return BulkGenericType.Leather;

				return BulkGenericType.Cloth;
			}
			else if ( deedType == BODType.Fletcher )
			{
				if ( itemType == null || itemType.IsSubclassOf( typeof( BaseRanged ) ) )
					return BulkGenericType.Wood;

				return BulkGenericType.Wood;
			}

			return BulkGenericType.Iron;
		}
	}
}