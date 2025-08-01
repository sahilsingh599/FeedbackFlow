import { FeedbackTable } from '@/components/FeedbackTable';
import { FeedbackItem } from '@/types';
import apiClient from '@/lib/api';

// This function fetches data on the server before rendering the page.
async function getFeedbackData(): Promise<FeedbackItem[]> {
    try {
        // Make the API call to our .NET backend.
        const response = await apiClient.get<FeedbackItem[]>('/feedback');
        return response.data;
    } catch (error) {
        console.error('Failed to fetch feedback data:', error);
        // In a real app, you'd handle this more gracefully.
        return [];
    }
}

export default async function HomePage() {
    const feedbackItems = await getFeedbackData();

    return (
        <div>
            <h1 className="text-3xl font-bold mb-6">Feedback Inbox</h1>
            <p className="mb-8 text-zinc-400">
                All customer feedback from your connected sources, in one place.
            </p>
            <FeedbackTable items={feedbackItems} />
        </div>
    );
}