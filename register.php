<?php
	//$con = mysqli_connect('localhost','root','root','snowball');
	$con = mysqli_connect('snowball.cy21f2pqg50l.us-west-2.rds.amazonaws.com','snowball','snowball','snowball');
	if (mysqli_connect_errno())
	{
		echo "Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$email = $_POST["email"];
	$password = $_POST["password"];

	$emailcheckquery = "select email from user where email = '".$email."';";
	$emailcheck = mysqli_query($con, $emailcheckquery) or die("Email check failed");

	if (mysqli_num_rows($emailcheck) > 0)
	{
		echo "Email already exists";
		exit();
	}

	$usernamecheckquery = "select username from user where username = '".$username."';";
	$usernamecheck = mysqli_query($con, $usernamecheckquery) or die("Username check failed.");

	if (mysqli_num_rows($usernamecheck) > 0)
	{
		echo "Username already exists";
		exit();
	}

	$salt = "\$5\$rounds=5000\$"."pwxeiock".$username."\$";
	$hash = crypt($password, $salt);

	$insertuserquery = "insert into user (username, email, hashed_password, salt) values ('".$username."','".$email."','".$hash."','".$salt."');";
	mysqli_query($con, $insertuserquery) or die ("User insert failed");
	echo "0";
?>