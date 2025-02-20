# Create directory for certificates
New-Item -ItemType Directory -Force -Path ./certs

# Generate CA key and certificate
openssl genrsa -out ./certs/ca.key 4096
openssl req -new -x509 -key ./certs/ca.key -sha256 -subj "/CN=RocketOps-DevCA" -days 365 -out ./certs/ca.crt

# Generate server key and CSR
openssl genrsa -out ./certs/server.key 2048
openssl req -new -key ./certs/server.key -out ./certs/server.csr -subj "/CN=localhost"

# Create config file with Subject Alternative Names
@"
authorityKeyIdentifier=keyid,issuer
basicConstraints=CA:FALSE
keyUsage = digitalSignature, nonRepudiation, keyEncipherment, dataEncipherment
subjectAltName = @alt_names

[alt_names]
DNS.1 = localhost
DNS.2 = gateway
DNS.3 = frontend
DNS.4 = alerts-service
DNS.5 = monitoring-service
DNS.6 = reporting-service
"@ | Out-File -FilePath ./certs/server.ext -Encoding ascii

# Generate server certificate
openssl x509 -req -in ./certs/server.csr -CA ./certs/ca.crt -CAkey ./certs/ca.key -CAcreateserial `
    -out ./certs/server.crt -days 365 -sha256 -extfile ./certs/server.ext

# Generate PFX file for ASP.NET Core
openssl pkcs12 -export -out ./certs/aspnetapp.pfx -inkey ./certs/server.key -in ./certs/server.crt `
    -password pass:password

# Create Nginx compatible combined certificate
Get-Content ./certs/server.key, ./certs/server.crt | Out-File -FilePath ./certs/server.pem -Encoding ascii

# Cleanup CSR file
Remove-Item ./certs/server.csr
Remove-Item ./certs/server.ext

Write-Host "Development certificates generated successfully!"
Write-Host "Remember to add ./certs/ca.crt as a trusted root authority on your development machine."
