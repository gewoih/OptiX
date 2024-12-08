import {apiService} from './api.ts'
import type { PriceBar } from '@/models/PriceBar.ts'
import type { AxiosResponse } from 'axios'

class MarketDataService {
  async get(): Promise<PriceBar[]> {
    try {
      const response: AxiosResponse<PriceBar[]> = await apiService.axiosInstance.get('/market-data', {
        params: {
          symbol: 'BTCUSDT',
          from: '2024-12-07',
          to: '2024-12-09',
          timeFrame: 2
        }
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching meals plan', error);
      return [];
    }
  }
}

export const marketDataService = new MarketDataService();
