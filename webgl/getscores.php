<?php
	ini_set("display_errors", "1");

	error_reporting(E_ALL);

	$con = mysqli_connect('snowball.cy21f2pqg50l.us-west-2.rds.amazonaws.com','snowball','snowball','snowball');
	if (mysqli_connect_errno())
	{
		echo "Connection Failed.";
		exit();
	}

	$level = $_POST["level"];
	$sort = $_POST["sort"];

	if ($sort == "score")
	{
		$levelcheckquery = "select * from level where level = '".$level."' order by score desc limit 10;";
	}
	else
	{
		$levelcheckquery = "select * from level where level = '".$level."' order by time limit 10;";
	}

	$levelcheck = mysqli_query($con, $levelcheckquery) or die("Level check failed");

    while ($row = mysqli_fetch_assoc($levelcheck))
    {
		echo $row['username'] . "\t" . $row['score'] . "\t" . $row['time']. "\t" . $row['gems'] . "\n";
    }
?>