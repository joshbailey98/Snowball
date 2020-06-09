<?php
	//$con = mysqli_connect('localhost','root','root','snowball');
	$con = mysqli_connect('snowball.cy21f2pqg50l.us-west-2.rds.amazonaws.com','snowball','snowball','snowball');
	if (mysqli_connect_errno())
	{
		echo "Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$password = $_POST["password"];

	$usernamecheckquery = "select username, salt, hashed_password from user where username = '".$username."';";
	$usernamecheck = mysqli_query($con, $usernamecheckquery) or die("Username check failed.");

	if (mysqli_num_rows($usernamecheck) != 1)
	{
		echo "Username does not exist";
		exit();
	}

	$logininfo = mysqli_fetch_assoc($usernamecheck);
	$salt = $logininfo["salt"];
	$hashed_password = $logininfo["hashed_password"];

	$entered_hashed_password = crypt($password, $salt);
	if ($entered_hashed_password != $hashed_password)
	{
		echo "Incorrect password";
		exit();
	}

	echo "0";
?>