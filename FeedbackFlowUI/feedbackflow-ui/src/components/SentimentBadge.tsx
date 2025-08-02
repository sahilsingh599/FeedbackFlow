interface SentimentBadgeProps {
  sentiment?: string;
}

export function SentimentBadge({ sentiment }: SentimentBadgeProps) {
  const sentimentStyles: { [key: string]: string } = {
    Positive: 'bg-green-800 text-green-200 border-green-600',
    Negative: 'bg-red-800 text-red-200 border-red-600',
    Neutral: 'bg-zinc-700 text-zinc-200 border-zinc-500',
  };

  const style = sentiment ? sentimentStyles[sentiment] : sentimentStyles['Neutral'];
  
  if (!sentiment) {
    // Render a placeholder while waiting for analysis
    return <span className="px-2.5 py-0.5 text-xs font-semibold rounded-full border bg-zinc-800 text-zinc-400 border-zinc-700">...</span>;
  }

  return (
    <span className={`px-2.5 py-0.5 text-xs font-semibold rounded-full border ${style}`}>
      {sentiment}
    </span>
  );
}