while ($true) {
    $id = Read-Host "id:"
    $weapons = Read-Host "weapons:"
    $camo = Read-Host "camo:"
    $map = Read-Host "map:"
    $numberofkills = Read-Host "number of kills:"
    $tags = Read-Host "tags:"
    $player = Read-Host "player:"
    $timeoffrag = Read-Host "timeoffrag:"

    $xmlOutput = @"
<entry id="$id">
    <weapons>$weapons</weapons>
    <map>$map</map>
    <camo>$camo</camo>
    <numberofkills>$numberofkills</numberofkills>
    <tags>$tags</tags>
    <player>$player</player>
    <timeoffrag>$timeoffrag</timeoffrag>
    <url>xxxxx</url>
    <downloadurl>xxxxx</downloadurl>
</entry>
"@

    $xmlOutput | Set-Clipboard

    Write-Host "XML entry copied to clipboard."

    $useless = Read-Host 'Press Enter to continue...';

    $title = $weapons + ", " + $map + ", " + $numberofkills + "k, " + $tags + ", " + $player + ", " + $timeoffrag

    $title | Set-Clipboard

$utubelink = Read-Host "youtube link:"
# Remove the prefix
$utubelink = $utubelink -replace "https://youtu.be/", ""
$utubelink | Set-Clipboard


$drivelink = Read-Host "drivelink:"
# Remove the prefix
$drivelink = $drivelink -replace "https://drive.google.com/file/d/", ""

# Remove the suffix
$drivelink = $drivelink -replace "/view\?usp=drive_link", ""

$drivelink | Set-Clipboard
}