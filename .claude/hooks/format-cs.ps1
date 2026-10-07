$f = ($input | Out-String | ConvertFrom-Json).tool_input.file_path
if ($f -like '*.cs' -and $f -notmatch 'Migrations') { dotnet format dotnet_api.csproj --include $f | Out-Null }
exit 0
