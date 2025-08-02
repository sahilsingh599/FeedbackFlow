import axios from 'axios';
import https from 'https';
import fs from 'fs';
import path from 'path';

// Create an axios instance configured to talk to your .NET backend.
// The URL should match the one your backend is running on (check your terminal).
const apiClient = axios.create({
    baseURL: 'https://localhost:7212/api', // <-- IMPORTANT: Update this port if yours is different!
    headers: {
        'Content-Type': 'application/json',
    },
});

// For Node.js environments (like Next.js server-side), handle self-signed certificates
if (typeof window === 'undefined') {
    // We're on the server side
    try {
        const certPath = path.join(process.cwd(), 'certs', 'aspnetcore-https.pem');
        const MY_CA_BUNDLE = fs.readFileSync(certPath);
        
        const httpsAgent = new https.Agent({ 
            ca: MY_CA_BUNDLE,
            // For development, we can be less strict about hostname verification
            checkServerIdentity: () => undefined,
        });
        
        apiClient.defaults.httpsAgent = httpsAgent;
    } catch (error) {
        console.warn('Could not load certificate file, falling back to unsafe SSL:', error instanceof Error ? error.message : String(error));
        // Fallback to the previous approach if certificate file is not found
        apiClient.defaults.httpsAgent = new https.Agent({
            rejectUnauthorized: false
        });
    }
}

export default apiClient;