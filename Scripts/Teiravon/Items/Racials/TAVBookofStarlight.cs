using System;
using Server;

namespace Server.Items
{
	public class BookofStarlight : BaseBook
	{

		[Constructable]
//		public BookofStarlight() : base( 0x2259 )
		public BookofStarlight() : base( 0x2259, "Book of Starlight", "The Librarian", 38, false )
		{

			Hue = 2253;
			Name = "Book of Starlight";
			Weight = 5;
//			Writable = false;
			
			Pages[0].Lines = new string[]
				{
					"Honored reader,",
					"What you are to read",
					"in these pages are",
					"tales of all that is",
					"of the star born,",
					"worthy of notice.",
					"Within these pages,",
					"the history, present"
				};

			Pages[1].Lines = new string[]
				{
					"and future shall be",
					"woven into one fabric",
					"that will answer",
					"questions that were",
					"never answered.",
					"",
					"",
					"- The Librarian"
				};

			Pages[2].Lines = new string[]
				{
					"",
					"",
					"     Book of",
					"",
					"      Starlight",
					"",
					"",
					""
				};

			Pages[3].Lines = new string[]
				{
					"And so it came to be,",
					"that the first",
					"everlord of elves was",
					"crowned, selected by",
					"great Valar",
					"themselves, sent to us",
					"from the summerlands",
					"to yet again serve his"
				};

			Pages[4].Lines = new string[]
				{
					"people.  Everlord",
					"Ramula Ky'ta helped",
					"his people prosper,",
					"and was the",
					"constructor that drew",
					"the first blueprints",
					"of the elven trade",
					"frigates, as sleek and"
				};

			Pages[5].Lines = new string[]
				{
					"and elegant as a silver",
					"dagger, flying through",
					"the oceans.",
					"With Everlord Ramula's",
					"guidance, elven towers",
					"sprouted from the",
					"earth, trade vessels",
					"spread across the"
				};

			Pages[6].Lines = new string[]
				{
					"oceas, and Marandor,",
					"the homelands,",
					"flourished like never",
					"seen before.",
					"",
					"",
					"...",
					""
				};

			Pages[7].Lines = new string[]
				{
					"Today, Everlord Ramula",
					"was found dead from",
					"food poisoning, which",
					"sounds too illogical",
					"to be true.  Master",
					"cook Shau'vas of the",
					"drow clains no one was",
					"near any of the"
				};

			Pages[8].Lines = new string[]
				{
					"cooking pots during",
					"the making of the",
					"night's dinner, and",
					"that it must have been",
					"bad products brought",
					"from the wood elves.",
					"",
					"..."
				};

			Pages[9].Lines = new string[]
				{
					"No one guilty have been",
					"found from the",
					"tragedy, and the fact",
					"the reincarnated",
					"Everlord has fallen,",
					"is causing great",
					"sorrow amongst our",
					"kin.  Everlord Ramula"
				};

			Pages[10].Lines = new string[]
				{
					"shall be sent to our",
					"ladies awaiting arms",
					"from the flames of",
					"final release, and",
					"leader of our kin",
					"shall be Prince",
					"Caradyia Ky'ta",
					"firstborn of Ramula."
				};

			Pages[11].Lines = new string[]
				{
					"...",
					"",
					"",
					"Caradyia has shown to",
					"be a kind leader, but",
					"far too interested in",
					"crafting than",
					"politics.  To his help,"
				};

			Pages[12].Lines = new string[]
				{
					"he has appointed a",
					"council, that brings",
					"him the most important",
					"matters, while he,",
					"himself spends his time",
					"with perfecting his",
					"skills as a smith and",
					"mason."
				};

			Pages[13].Lines = new string[]
				{
					"...",
					"",
					"Today a horde of ice",
					"beasts attacked the",
					"northern borders of",
					"our land, and have",
					"spread south, as if",
					"something is agitating",
				};		

			Pages[14].Lines = new string[]
				{
					"them to attack our",
					"lands.  A meeting of",
					"all kin has been ",
					"arranged.  The lack of",
					"an elven army makes",
					"itself heard.",
					"",
					"..."
				};		

			Pages[15].Lines = new string[]
				{
					"Prince Caradyia begins",
					"his great project of",
					"the great defense.",
					"This includes",
					"gathering blacksmiths",
					"handpicked by His",
					"Majesty himself, and",
					"to begin forging"
				};		

			Pages[16].Lines = new string[]
				{
					"enough armours and",
					"weapons to equip the",
					"entire Elven race, in",
					"times of war.  Where",
					"the halls great enough",
					"to store a mass of",
					"objects like this will",
					"be is - only Prince"
				};		

			Pages[17].Lines = new string[]
				{
					"Caradyia himself will",
					"know.",
					"",
					"...",
					"",
					"The amount of",
					"masterpieces from the",
					"Caradyia forges are"
				};		

			Pages[18].Lines = new string[]
				{
					"steady, but no one",
					"still knows where",
					"they go.  It is",
					"rumored that the",
					"Prince had crafted",
					"deep catacombs",
					"beneath the city",
					"itself, but nothing is"
				};		

			Pages[19].Lines = new string[]
				{
					"sure and the Prince",
					"himself speaks not of",
					"where he keeps this ",
					"treasury of his.",
					"",
					"...",
					"",
					"The wife of Prince"
				};		

			Pages[20].Lines = new string[]
				{
					"Caradyia, Melina, has",
					"become pregnant and",
					"the citizens are",
					"already organizing a",
					"festival of",
					"celebration.",
					"",
					"..."
				};		

			Pages[21].Lines = new string[]
				{
					"The dances are",
					"enchanting, dragging",
					"you with them in pure",
					"ecstasy.  Around the",
					"glade the blue fires",
					"of the marshals blow",
					"in the wind.",
					""
				};		

			Pages[22].Lines = new string[]
				{
					"...",
					"Melina died while",
					"giving birth. Caradyia",
					"is not taking it well.",
					"",
					"...",
					"Caradyia left this",
					"morning, taking a ship"
				};		

			Pages[23].Lines = new string[]
				{
					"to self-exile himself,",
					"leaving his son Sagas",
					"in the hands of the",
					"clergy to raise.",
					"",
					"...",
					"",
					"We truly are the lords"
				};		

			Pages[24].Lines = new string[]
				{
					"and ladies of star,",
					"ocean and earth. All",
					"is prosperous and",
					"well. Most spending",
					"the days in the",
					"gardens, drinking",
					"faewine.",
					""
				};		

			Pages[25].Lines = new string[]
				{
					"...",
					"Some cleric spoke of a",
					"bad omen, a punishment",
					"of the gods. Why would",
					"they want to punish",
					"us? It is surely",
					"nonesense of a cleric",
					"who has had too much"
				};		

			Pages[26].Lines = new string[]
				{
					"faewine.  Speaking of",
					"that, the brewer",
					"Galris delivered yet",
					"another handful of",
					"barrels to our",
					"gardens. He surely is",
					"a saint.",
					""
				};		

			Pages[27].Lines = new string[]
				{
					"...",
					"",
					"Heard news of some",
					"new tribe of monstrous",
					"barbarians attacking",
					"the wood elves of",
					"Tihr'indo.  Orcs, they",
					"were called. Maybe the"
				};		

			Pages[28].Lines = new string[]
				{
					"omen was true, after",
					"all. Nevertheless, the",
					"wood elves are sure to",
					"take care of it.",
					"",
					"...",
					"",
					"They are coming! All"
				};		

			Pages[29].Lines = new string[]
				{
					"have retreated to",
					"Arandor for a final",
					"stand, while the mages",
					"have locked themselves",
					"up in the tower for",
					"some reason. Prince",
					"Sagas has taken all",
					"sentinels to defend"
				};		

			Pages[30].Lines = new string[]
				{
					"our lands.",
					"",
					"...",
					"",
					"I am writing what",
					"might be the last",
					"recording in this",
					"book. I have taken the"
				};		

			Pages[31].Lines = new string[]
				{
					"Prince's two sons,",
					"Belarius and Kaethe,",
					"and my own daughter",
					"and husband to the",
					"temple of Loren. It",
					"seems we are behind",
					"enemy lines. We shall",
					"leave on a small raft"
				};		

			Pages[32].Lines = new string[]
				{
					"this morning. Dear",
					"Loren do not let them",
					"catch us. My daughter",
					"Ainure is just",
					"tumbling in her sleep,",
					"and the two sons are",
					"wary. How will this",
					"end?"
				};		

			Pages[33].Lines = new string[]
				{
					"...",
					"",
					"They have all grown to",
					"be such prides of the",
					"Elvenkin. Ainure and",
					"Kaethe spend most of",
					"the days playing,",
					"while Belarius mostly"
				};		

			Pages[34].Lines = new string[]
				{
					"sits by a tree,",
					"dreaming. Such a",
					"strange trio they are,",
					"but I love them all",
					"nevertheless. We have",
					"grown quite fond of",
					"our at first momentary",
					"shelter. A small patch"
				};		

			Pages[35].Lines = new string[]
				{
					"of farmland, and some",
					"chickens and a goat.",
					"It is not much, but it",
					"is more than enough,",
					"and we thank our",
					"ladies for it every",
					"evening. However, I",
					"cannot but wonder if"
				};		

			Pages[36].Lines = new string[]
				{
					"Arandor still stands,",
					"if Prince Sags fell",
					"or not. If he did, I",
					"do not know, and",
					"neither what happened.",
					"What I do know is that",
					"there is nothing more",
					"I can add to this"
				};

			Pages[37].Lines = new string[]
				{
					"tome. Tomorrow, I",
					"shall go hide it where",
					"it will be, until it",
					"calls for the ink of",
					"recording again.",
					"",
					"",
					""
				};		
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			list.Add( "Book of Starlight" );
		}

		public override void OnSingleClick( Mobile from )
		{
			LabelTo( from, "Book of Starlight" );
		}

		public BookofStarlight( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
