<script lang="ts">
    import { onMount } from 'svelte';
    import Chart from 'chart.js/auto';
    import {onMarketDataReceived, startConnection, subscribeToSymbol, unsubscribeFromSymbol} from "$lib/signalrClient";
    import type {TickData} from "../../types/tickData";

    let chart: Chart;
    let labels: string[] = [];
    let prices: number[] = [];

    const createChart = (): void => {
        const ctx = document.getElementById('marketDataChart') as HTMLCanvasElement;
        chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Market Price',
                        data: prices
                    }
                ]
            }
        });
    };

    const updateChart = (tick: TickData): void => {
        labels.push(tick.dateTime.toString());
        prices.push(tick.price);

        chart.update();
    };

    onMount(async () => {
        createChart();

        await startConnection();

        onMarketDataReceived((tick: TickData) => {
            updateChart(tick);
        });
    });
</script>

<style>
    canvas {
        max-width: 100%;
        height: auto;
    }
</style>

<div>
    <h1>Live Market Data</h1>
    <canvas id="marketDataChart"></canvas>
    <button on:click={async () => await subscribeToSymbol('BTCUSDT')} class="btn variant-filled">Подписаться на BTC/USDT</button>
    <button on:click={async () => await unsubscribeFromSymbol('BTCUSDT')}>Отписаться от BTC/USDT</button>
</div>
