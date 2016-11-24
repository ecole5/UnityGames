using UnityEngine;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

public class Utility : MonoBehaviour {

	static string temp;

    //Check if alphanumeric
	public static bool IsAlphaNumeric(string strToCheck)
	{
		Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
		return !objAlphaNumericPattern.IsMatch(strToCheck);
	}
    
    //Create a MD5 hash function in the same format as PHP hash function
	public static string newHash (string secretKey){
	MD5 md5 = MD5CryptoServiceProvider.Create ();
	byte[] dataMd5 = md5.ComputeHash (Encoding.Default.GetBytes (secretKey));
	StringBuilder sb = new StringBuilder();
	for (int i = 0; i < dataMd5.Length; i++)
		sb.AppendFormat("{0:x2}", dataMd5[i]);
	return sb.ToString ();
}


}
