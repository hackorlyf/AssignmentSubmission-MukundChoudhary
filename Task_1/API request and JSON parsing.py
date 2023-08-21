import requests

# Make API request
api_url = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data"
response = requests.get(api_url)
json_data = response.json()

# Parse JSON data
clients = json_data.get("clients", [])

# Filter clients based on selected option
def filter_clients(option):
    if option == "All clients":
        return clients
    elif option == "Managers only":
        return [client for client in clients if client["is_manager"]]
    elif option == "Non-managers":
        return [client for client in clients if not client["is_manager"]]

# Example client structure in JSON: {"name": "John Doe", "points": 100, "address": "123 Main St", "is_manager": True}