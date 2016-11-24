[System.Serializable]
//Preferences object for portal, the only preferences are background and music 
public class PortalPrefs {
	public int backgroundChoice, musicChoice;
	public float musicVolume;
	public PortalPrefs(){
		//set defaults 
		backgroundChoice = 0;
		musicChoice = 1;
		musicVolume = 1f;
	}
}
