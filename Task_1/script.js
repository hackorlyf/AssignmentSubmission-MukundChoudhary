const clientsData = {
    "clients": [
      {"isManager":true,"id":1,"label":"Client1"},
      {"isManager":false,"id":2,"label":"Client2"},
      {"isManager":false,"id":3,"label":"Client3"},
      {"isManager":false,"id":4,"label":"Client4"},
      {"isManager":false,"id":5,"label":"Client5"}
    ],
    "data": {
      "1": {"address":"NY","name":"Jhon","points":123},
      "2": {"address":"NY","name":"Dan","points":123},
      "3": {"address":"NY","name":"Ben","points":123}
    },
    "label": "All Clients"
  };
  
  const clientList = document.getElementById("client-list");
  const filterDropdown = document.getElementById("filter-dropdown");
  const popup = document.getElementById("popup");
  const popupName = document.getElementById("popup-name");
  const popupPoints = document.getElementById("popup-points");
  const popupAddress = document.getElementById("popup-address");
  
  // Populate initial client list
  function populateClientList(filter) {
    clientList.innerHTML = "";
  
    clientsData.clients.forEach(client => {
      if (filter === "all" || (filter === "managers" && client.isManager) || (filter === "non-managers" && !client.isManager)) {
        const li = document.createElement("li");
        li.textContent = `${client.label} - Points: ${clientsData.data[client.id].points}`;
        li.addEventListener("click", () => showPopup(client.id));
        clientList.appendChild(li);
      }
    });
  }
  
  // Show popup with client details
  function showPopup(clientId) {
    const client = clientsData.data[clientId];
    popupName.textContent = client.name;
    popupPoints.textContent = `Points: ${client.points}`;
    popupAddress.textContent = `Address: ${client.address}`;
    popup.style.display = "block";
  }
  
  // Event listener for filter dropdown change
  filterDropdown.addEventListener("change", () => {
    const selectedFilter = filterDropdown.value;
    populateClientList(selectedFilter);
  });
  
  // Initial population of client list (default: All clients)
  populateClientList("all");
  