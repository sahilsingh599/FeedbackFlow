export interface FeedbackItem {
    id: string;
    source: string;
    content: string;
    author: string;
    receivedAt: string; // Comes as an ISO string from JSON
    sentiment?: string;
    topics?: string[];
    isAnalyzed: boolean;
}