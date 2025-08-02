// Make sure the path below matches the actual location of SentimentChart.tsx or .js
import { SentimentChart } from '../../components/SentimentChart';
import apiClient from '@/lib/api';
import { SentimentDistribution } from '@/types';

async function getSentimentDistribution(): Promise<SentimentDistribution[]> {
  try {
    const response = await apiClient.get<SentimentDistribution[]>('/analytics/sentiment-distribution');
    return response.data;
  } catch (error) {
    console.error('Failed to fetch sentiment distribution:', error);
    return [];
  }
}

export default async function AnalyticsPage() {
  const data = await getSentimentDistribution();

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Analytics Dashboard</h1>
      <p className="mb-8 text-zinc-400">
        A high-level overview of your customer feedback.
      </p>
      
      <div className="bg-zinc-950 p-6 rounded-lg border border-zinc-800">
         <h2 className="text-xl font-semibold mb-4">Sentiment Distribution</h2>
         <div style={{ width: '100%', height: 300 }}>
            <SentimentChart data={data} />
         </div>
      </div>
    </div>
  );
}