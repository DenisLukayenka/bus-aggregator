let serverEndpoint = window.location.href;

if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
    serverEndpoint = "http://localhost:5087/";
    console.log("dev env");
}

const apiEndpoints = serverEndpoint + 'api/';
const mapEndpoint = apiEndpoints + 'map/';

export const AppConfig = {
    serverEndpoint,
    mapEndpoint: {
        url: mapEndpoint,
        mapUrl: mapEndpoint + 'metadata'
    }
}