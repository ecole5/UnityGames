<?php
    //Establish conection to elcstone_db
        $db = new mysqli("108.167.189.18", "elcstone_admin", "password", "elcstone_db");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	//Get hash from client and verify against calculated hash (they both use the same secret key)
	 $givenHash = ($_POST['hash']); 
	 $secretKey = "DrOuda";
	 $realHash = md5($secretKey);
	 if ($realHash != $givenHash) //if hash is not the same close the connection
	 {
		die('Imposter Detected');
	 }
	 
	 
	 //Get the username and password if hash checks out 
	 $username = ($_POST['username']);
     $password = ($_POST['password']); 
	


	//Note you need the single quotes or the query will fail as thats show it knows its text
    $query = "SELECT * FROM users WHERE BINARY username = '$username'"; //uses variable interpolation (detedcts variable in string)
    $result = $db->query($query);
 

	if  ($result-> num_rows == 0){ //result returns false if no rows found
	
		echo "Username Not Found";
	}
	else {
		
		$row = mysqli_fetch_array($result); //default array type will be both associative and indexed
		if ($row['tries'] == 0){
			echo "Account Blocked";
		}
		
		
		else if ($password == $row['password']){ //we use the associative key to get the value //note passwords are case insensative
			echo "Login Successful";
		}
		else if ($username == "admin")//we never block the admin
		{
			echo "Incorrect Password"; 
		}
		else{ //display number of login attempts left and adjust database
			$temp = $row['tries'] -1;
			if ($temp == 1)
			{
				echo "Incorrect Password: Last Attempt";
			}
			else if ($temp == 0)
			{
				echo "Blocked";
			}
			else{
				echo "Incorrect Password: $temp Attempts Remain";
			}
			
			//Adjust tries 
			$query = "UPDATE users SET tries = $temp WHERE username = '$username'";
			$db->query($query);
		}
	
	}

	
	$db ->close(); //close the connection
   
?>