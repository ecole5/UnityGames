<?php
    //Establish conection to elcstone_db
		$db = new mysqli("127.0.0.1", "admin", "password", "ugames");

	//Check connection
	if ($db -> connect_error){ //if error attribute true
		die('Connection To DB Failed');
	}

	 //Get the data from the form
	 $username = ($_POST['username']);
     $password = ($_POST['password']); 
	
	//Query the database and update the pasword
	$query = "UPDATE users SET password = '$password'  WHERE username = '$username'";
    $result = $db->query($query);

	$db ->close(); //close the connection
   
?>