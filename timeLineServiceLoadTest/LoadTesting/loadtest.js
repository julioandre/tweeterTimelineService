export {getTimelineLoadTest} from './getTimelineLoadTest.js';

export const options = {
    scenarios: {
        scenariopopulateTimeline: {
            executor: 'shared-iterations',
            exec: 'getTimelineLoadTest',
            vus: 10,
            iterations: 200,
        },
    }
}