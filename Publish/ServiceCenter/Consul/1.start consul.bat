:: consul.exe agent -server ui -bootstrap -client 0.0.0.0 -data-dir="consul_data" -bind 0.0.0.0
:: consul agent -dev
 
consul agent -dev -config-file "consul.json"

pause