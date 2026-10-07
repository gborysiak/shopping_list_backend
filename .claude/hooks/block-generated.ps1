$f = ($input | Out-String | ConvertFrom-Json).tool_input.file_path
if ($f -match '[\/]Migrations[\/]|[\/](bin|obj)[\/]') {
  [Console]::Error.WriteLine("Generated file: use 'dotnet ef' (see add-migration skill), don't edit by hand.")
  exit 2
}
exit 0
