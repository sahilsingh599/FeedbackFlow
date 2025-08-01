import axios from 'axios';
import https from 'https';

// Create an axios instance configured to talk to your .NET backend.
// The URL should match the one your backend is running on (check your terminal).
const apiClient = axios.create({
    baseURL: 'https://localhost:7212/api', // <-- IMPORTANT: Update this port if yours is different!
    headers: {
        'Content-Type': 'application/json',
    },
    // Allow self-signed certificates for development
    httpsAgent: new https.Agent({
        rejectUnauthorized: false
    })
});

export default apiClient;