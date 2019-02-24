export class YTRepsone {
  kind: string;
  nextPageToken: string;
  prevPageToken: string;
  snippits: Snippet[]
  error: boolean;
}

export class Snippet {
  title: string;
  description: string;
  channelTitle: string;
  liveBroadcastContent: string;
  url: string;
  channelId: string;
  videoId: string;
}
