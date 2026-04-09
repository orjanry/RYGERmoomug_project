console.log("Search GlobalCollection.js loaded");

document.getElementById("search-form").addEventListener("submit", async function (e) {
    e.preventDefault(); // Forhindrer siden fra å laste på nytt

    const query = document.getElementById("search-input").value.trim(); // Hent brukerens input
    const resultsDiv = document.getElementById("results");

    if (!query) {
        resultsDiv.innerHTML = "<p>Please enter a mug name to search.</p>";
        return;
    }

    try {
        // Send GET-forespørsel til API-et
        console.log("Sending fetch request to:", `/GlobalCollection/search?query=${encodeURIComponent(query)}`);
        const response = await fetch(`/GlobalCollection/search?query=${encodeURIComponent(query)}`);
        console.log("Response status:", response.status);
        if (!response.ok) {
            const errorText = await response.text();
            resultsDiv.innerHTML = `<p>Error: ${errorText}</p>`;
            return;
        }

        // Hent JSON-resultatene
        const mugs = await response.json();
        console.log("Data received: ", mugs);

        // Vis resultatene i HTML
        if (mugs.length > 0) {
            resultsDiv.innerHTML = `
                <ul>
                    ${mugs.map(mug => `<li><img src="${mug.mugImage}" alt="${mug.mugName}"> ${mug.mugName} ${mug.startYear}</li>`).join('')}
                </ul>
            `;
        } else {
            resultsDiv.innerHTML = "<p>No mugs found.</p>";
        }
    } catch (error) {
        resultsDiv.innerHTML = `<p>Error: ${error.message}</p>`;
    }
});