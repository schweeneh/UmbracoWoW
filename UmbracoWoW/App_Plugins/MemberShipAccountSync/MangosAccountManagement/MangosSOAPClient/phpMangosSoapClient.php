<?php PHP_SAPI === 'cli' or die('not allowed');

$mangosUser = $argv[1];
$mangosPassword = $argv[2];
$mangosHost = $argv[3];
$mangosPort = $argv[4];
$mangosCommand = $argv[5];

$client = new SoapClient(NULL, array(
    'location' => "http://$mangosHost:$mangosPort/",
    'uri'      => 'urn:MaNGOS',
    'style'    => SOAP_RPC,
    'login'    => $mangosUser,
    'password' => $mangosPassword,
));

try {
    $result = $client->executeCommand(new SoapParam($mangosCommand, "command"));
    echo "Command succeeded! Output:<br />\n";
    echo $result;
}
catch (Exception $e)
{
    echo "Command failed! Reason:<br />\n";
	fwrite(STDERR, $e->getMessage());
}

?>