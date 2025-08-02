'use client'; // This component uses hooks and event handlers, so it must be a client component.

import { SentimentDistribution } from '@/types';
import { ResponsiveContainer, BarChart, CartesianGrid, XAxis, YAxis, Tooltip, Legend, Bar, Cell } from 'recharts';

interface SentimentChartProps {
  data: SentimentDistribution[];
}

// Define colors for the bars to match our sentiment badges
const sentimentColors: { [key: string]: string } = {
    Positive: '#22c55e', // green-500
    Negative: '#ef4444', // red-500
    Neutral: '#a1a1aa',  // zinc-400
};

export function SentimentChart({ data }: SentimentChartProps) {
  if (!data || data.length === 0) {
    return <p className="text-center text-zinc-500 mt-10">Not enough data to display chart.</p>;
  }
  
  return (
    <ResponsiveContainer width="100%" height="100%">
      <BarChart
        data={data}
        margin={{
          top: 5,
          right: 30,
          left: 20,
          bottom: 5,
        }}
      >
        <CartesianGrid strokeDasharray="3 3" stroke="#444" />
        <XAxis dataKey="sentiment" stroke="#999" />
        <YAxis allowDecimals={false} stroke="#999" />
        <Tooltip
            contentStyle={{ backgroundColor: '#222', border: '1px solid #444' }}
            labelStyle={{ color: '#fff' }}
        />
        <Legend />
        <Bar dataKey="count" name="Feedback Count">
            {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={sentimentColors[entry.sentiment] || '#ccc'} />
            ))}
        </Bar>
      </BarChart>
    </ResponsiveContainer>
  );
}