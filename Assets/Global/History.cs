[System.Serializable]
public class History
{
    //structure to hold the history objects that can be serialized by Json
	public int size; //current size of the list (number of non null entries since all are actually instated at start 
	public Record[] list; //needs to be simple structure like list or json will not serialize
	public int maxSize; //maximum size of the list (number of entries to store)
	public History()
	{
		maxSize = 10; //chosen to be 10
		list = new Record[maxSize]; //initialize the list
	}


    //Structure to encapsulate the date for each entry in the list 
	[System.Serializable]
	public class Record
	{
		public string username, date, level, time;
		public int score;
		public Record(string username , string date , int score , string level = null, string time = null)
		{
			this.username = username;
			this.date = date;
			this.score = score;
			this.level = level;
			this.time = time;
		}
		public Record(){
			score = -1; //must be less than zero for insert by score to work 
			username = null;
		}
	
	}
}
	
