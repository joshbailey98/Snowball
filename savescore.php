<?php
	//$con = mysqli_connect('localhost','root','root','snowball');
	$con = mysqli_connect('snowball.cy21f2pqg50l.us-west-2.rds.amazonaws.com','snowball','snowball','snowball');
	if (mysqli_connect_errno())
	{
		echo "Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$score = $_POST["score"];
	$gems = $_POST["gems"];
	$time = $_POST["time"];
	$level = $_POST["level"];
	$levelcheckquery = "select * from level where level = '".$level."' and username = '".$username."';";
	$levelcheck = mysqli_query($con, $levelcheckquery) or die("Level check failed");

	if (mysqli_num_rows($levelcheck) > 0)
	{
		$updatescorequery = "update level set score = '".$score."' where level = '".$level."' and username = '".$username."' and '".$score."' > score;";
		mysqli_query($con, $updatescorequery) or die ("Score update failed.");
		$updategemsquery = "update level set gems = '".$gems."' where level = '".$level."' and username = '".$username."' and '".$gems."' > gems;";
		mysqli_query($con, $updategemsquery) or die ("Gems update failed.");
		$updatetimequery = "update level set time = '".$time."' where level = '".$level."' and username = '".$username."' and '".$time."' < time;";
		mysqli_query($con, $updatetimequery) or die ("Time update failed.");
		echo "0";
	}
	else
	{
		$insertscorequery = "insert into level (level, username, score, gems, time) values ('".$level."','".$username."','".$score."','".$gems."','".$time."');";
		mysqli_query($con, $insertscorequery) or die ("Score insert failed.);");
		echo "0";
	}
?>