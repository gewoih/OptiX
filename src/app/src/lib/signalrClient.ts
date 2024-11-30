import * as signalR from "@microsoft/signalr";
import type {TickData} from "../types/tickData";

const backendUrl = import.meta.env.VITE_BACKEND_URL;
const signalrUrl = import.meta.env.VITE_SIGNALR_URL;

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${backendUrl}` + `${signalrUrl}`)
    .withAutomaticReconnect()
    .build();

export const startConnection = async () => {
    try {
        await connection.start();
        console.log("SignalR connected to", import.meta.env.VITE_BACKEND_URL);
    } catch (err) {
        console.error("SignalR connection error:", err);
    }
};

export const subscribeToSymbol = async(symbol: string) => {
    try {
        await connection.invoke("SubscribeToSymbol", symbol);
        console.log(`Subscribed to ${symbol} updates`);
    } catch (err) {
        console.error(`Error occured on subscription to ${symbol} updates`, symbol);
    }
}

export const unsubscribeFromSymbol = async(symbol: string) => {
    try {
        await connection.invoke("UnsubscribeFromSymbol", symbol);
        console.log(`Unsubscribed from ${symbol} updates`);
    } catch (err) {
        console.error(`Error occured on unsubscription from ${symbol} updates`, symbol);
    }
}

export const onMarketDataReceived = (callback: ((tick: TickData) => void)) => {
    connection.on("ReceiveMarketData", callback);
};