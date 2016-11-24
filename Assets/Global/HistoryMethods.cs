using System;

public class HistoryMethods {

    //History methods 
	
	//Regular history record update
	public static History.Record regLog(string score)
	{
		DateTime time = DateTime.Now;
		string temp2 = time.ToString(); //get current date time in nice format
		History.Record temp = new History.Record();
		temp.username = GameData.Prefs.username;
		temp.date = temp2;
		temp.score = int.Parse (score);
		return temp;
	}

	//Create a Space history record and add it to the space log
	public static History.Record spaceLog(string score, string level)
	{
		DateTime time = DateTime.Now;
		string temp2 = time.ToString(); //get current date time in nice format
		History.Record temp = new History.Record();
		temp.username = GameData.Prefs.username;
		temp.date = temp2;
		temp.score = int.Parse (score);
		temp.level = level;
		return temp;
	}

    //Create a history record and add it to the portal log
	public static History.Record portalLog(string startupTime)
	{
		DateTime time = DateTime.Now;
		string temp2 = time.ToString (); //get current date time in nice format
		History.Record temp = new History.Record();
		temp.username = GameData.Prefs.username;
		temp.date = temp2;
		temp.time = startupTime;
		return temp;
	}


	public static void addByDate(History.Record newRecord, History historyObject)
	{
		insertInPosition(0, newRecord, historyObject);
		if (historyObject.size != historyObject.maxSize){
			historyObject.size++;
		}
	}

	public static void addByScore(History.Record newRecord, History historyObject)
	{
		for (int i = 0; i < historyObject.maxSize; i++)
		{

			if (historyObject.list[i].score < newRecord.score || historyObject.list[i].username == null)
			{
				insertInPosition(i,newRecord, historyObject);
				if (historyObject.size != historyObject.maxSize)
				{
					historyObject.size++;
				}
				break;

			}
		}

	}

	public static void insertInPosition(int index, History.Record data, History historyObject)
	{
		for (int i = historyObject.maxSize - 2; i > index - 1; i--)
		{
			historyObject.list[i + 1] = historyObject.list[i];
		}
		historyObject.list[index] = data;
	}


	public static void remove(string name, History historyObject)
	{
		for (int i = 0; i < historyObject.maxSize; i++)
		{
			if (historyObject.list[i].username == name)
			{
				for (int e = i+1; e < historyObject.size; e++)
				{
					historyObject.list[e - 1] = historyObject.list[e];
				}
				historyObject.list[historyObject.size-1] = new History.Record();
				historyObject.size--;
			}
		}
	}
		

}
